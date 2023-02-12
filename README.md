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

### Build the image

```
docker build --tag dotnet-docker .
```
### Run the container

To publish a port for our container, we’ll use the `--publish` flag (`-p` for short) on the docker run command. The format of the `--publish` command is `[host port]:[container port]`. So, if we wanted to expose port 80 inside the container to port 5000 outside the container, we would pass 5000:80 to the `--publish` flag. Run the container using the following command:

```
 docker run --publish 80:8080 dotnet-docker
```
Meaning that inside the container it listens on port 8080 but accessing it from outside it is on port 80
