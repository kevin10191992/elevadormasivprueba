# To run test

dotnet test .\Elevador\Elevador.sln

# To run the app whit docker and docker-compose - the aplication port is 5000

docker-compose -f "Elevador\docker-compose.yml" up -d --build