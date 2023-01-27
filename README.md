# Warehouse App

This repository is a solution for a task assignment defined in `Full Stack Assignment Description.pdf`

The app is currently running [here](https://app-v1-bhlby76vvq-uc.a.run.app/swagger/index.html).

## Technologies:
- ASP.NET Web API framework v.7.0
- Google Sheets as a database to store products and artifacts
- Docker to containerize the app 
- `Google Cloud Run` for deployment

## To test it locally:

1. Clone the repo and `cd` into it 

```
git clone https://github.com/lukyrasocha/warehouse-app.git
cd warehouse-app
```

2. Run the app

```
dotnet run
```

3. Test it out http://0.0.0.0:8080/api/product


## To deploy the app to Google Cloud

