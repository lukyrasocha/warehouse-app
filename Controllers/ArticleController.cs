using Microsoft.AspNetCore.Mvc;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using warehouse_app.utils;
using warehouse_app.Models;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace warehouse_app.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    const string SPREADSHEET_ID = "11sdhcwjYtp-MyZ0SsTSn2tp2Rkq-IuwRnP6FAlAWKME";
    const string SHEET_NAME = "Inventory";

    SpreadsheetsResource.ValuesResource _googleSheetValues;

    public ArticleController(GoogleSheetsHelper googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var range = $"{SHEET_NAME}!A:C";

        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;

        return Ok(ArticleMapper.MapFromRangeData(values));
    }

    [HttpGet("{rowId}")]
    public IActionResult Get(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;

        return Ok(ArticleMapper.MapFromRangeData(values).FirstOrDefault());
    }

    [HttpPost]
    public IActionResult Post(Article article)
    {
        var range = $"{SHEET_NAME}!A:C";
        var valueRange = new ValueRange
        {
            Values = ArticleMapper.MapToRangeData(article)
        };

        var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
        appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();

        return CreatedAtAction(nameof(Get), article);
    }

    [HttpPut("{rowId}")]
    public IActionResult Put(int rowId, Article article)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var valueRange = new ValueRange
        {
            Values = ArticleMapper.MapToRangeData(article)
        };

        var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
        updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();

        return NoContent();
    }

    [HttpDelete("{rowId}")]
    public IActionResult Delete(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var requestBody = new ClearValuesRequest();

        var deleteRequest = _googleSheetValues.Clear(requestBody, SPREADSHEET_ID, range);
        deleteRequest.Execute();

        return NoContent();
    }

    [Route("testGet")]
    [HttpGet]
    public IActionResult testGet(){

        var helper = new GoogleSheetsHelper();
        var controller = new ProductController(helper);
        
        var x = (OkObjectResult) controller.Get();
        var actual = x.Value as List<Product>; 

        foreach (var article in actual![0].articles!){
            Console.WriteLine(article);
            Console.WriteLine(article.GetType().GetProperty("amount_of")!.GetValue(article, null));

        }
        return NoContent();

    }
}