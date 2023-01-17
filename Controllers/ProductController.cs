using Microsoft.AspNetCore.Mvc;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using warehouse_app.utils;
using warehouse_app.Models;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace warehouse_app.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    const string SPREADSHEET_ID = "11sdhcwjYtp-MyZ0SsTSn2tp2Rkq-IuwRnP6FAlAWKME";
    const string SHEET_NAME = "Products";

    SpreadsheetsResource.ValuesResource _googleSheetValues;

    public ProductController(GoogleSheetsHelper googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var range = $"{SHEET_NAME}!A:B";

        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;

        return Ok(ProductMapper.MapFromRangeData(values));
    }

    [HttpGet("{rowId}")]
    public IActionResult Get(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:B{rowId}";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;

        return Ok(ProductMapper.MapFromRangeData(values).FirstOrDefault());
    }

    [HttpPost]
    public IActionResult Post(Product product)
    {
        var range = $"{SHEET_NAME}!A:B";
        var valueRange = new ValueRange
        {
            Values = ProductMapper.MapToRangeData(product)
        };

        var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
        appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();

        return CreatedAtAction(nameof(Get), product);
    }

    [HttpPut("{rowId}")]
    public IActionResult Put(int rowId, Product product)
    {
        var range = $"{SHEET_NAME}!A{rowId}:B{rowId}";
        var valueRange = new ValueRange
        {
            Values = ProductMapper.MapToRangeData(product)
        };

        var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
        updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();

        return NoContent();
    }

    [HttpDelete("{rowId}")]
    public IActionResult Delete(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:B{rowId}";
        var requestBody = new ClearValuesRequest();

        var deleteRequest = _googleSheetValues.Clear(requestBody, SPREADSHEET_ID, range);
        deleteRequest.Execute();

        return NoContent();
    }
}