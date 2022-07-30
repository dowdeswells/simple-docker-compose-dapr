# simple-docker-compose-dapr
simple solution to get docker compose debugging up and running is VS and Rider

### Status
 - is able to be debugged in VS when using the docker-compose configuration
 - not tested in Rider yet

### Testing
can test the service with: `curl -v http://localhost:<someport>/weatherforecast`

### current problems are:
 - someport is randomly assigned every time i open VS
 - chrome insists on https when running on a windows machine
