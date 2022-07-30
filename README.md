# simple-docker-compose-dapr
simple solution to get docker compose debugging up and running is VS and Rider

### Status
 - is able to be debugged in VS when using the docker-compose configuration
 - Rider worked on windows after adding a docker compose configuration as detailed
here [Jetbrains howto debug docker compose](https://blog.jetbrains.com/dotnet/2020/01/13/docker-compose-edit-continue-c-8-debugger-updates-rider-2019-3/)
 - Rider on linux with the same instructional blog above applied did not work initially. 
Below are the steps to get it working

### Getting Rider to debug the docker compose on linux ... No HTTPS though

Applying the steps here [Jetbrains howto debug docker compose](https://blog.jetbrains.com/dotnet/2020/01/13/docker-compose-edit-continue-c-8-debugger-updates-rider-2019-3/) 
gave an error:

> Cannot run program "docker-compose" (in directory "/home/simon/dev/containerApps/simple-docker-compose-dapr"): error=2, No such file or directory

Couldnt find any info on the net as to where this should be on
a linux  system. I figured it might be installed somewhere so i searched for 
it on the drive and found it:
> `find /usr -name "docker-compose"`
> found the file below (but its possible i installed it at some point before):
> - /usr/libexec/docker/cli-plugins/docker-compose

So i set that path in Rider in the "Docker Compose Executable" textbox for
> File | Settings | Build, Execution, Deployment | Docker | Tools

Now this works but i have an issue with running the services needing to 
find a certificate for SSL. To fix this (not wanting to generate pfx files etc) 
I changed the mapping of urls in the environment variables, removing the https
mapping:
>  
>environment:
> - ASPNETCORE_URLS=http://+:80
>
I must also highlight that i commented out the https redirection in the c# code:
`//app.UseHttpsRedirection();`

### Testing
can test the service with: `curl -v http://localhost:<someport>/weatherforecast`

### current problems are:
 - someport is randomly assigned every time i open VS
 - chrome insists on https when running on a windows machine
