# DeleteUserInfoSaga

Esse repositório tem como objetivo exibir a implementação de uma SAGA Orquestrada utilizada para realizar a exclusão de dados de usuário em todos os serviços dos projetos. 
É necessário que seja feito via SAGA orquestrada por causa da ordem de execução de serviços, que deve ser uma ordem bem específica.

## Variáveis de ambiente
Todas as variáveis de ambiente do projeto visam fazer integração com algum serviço da AWS ou MercadoPago. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos serviços da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos serviços da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- AWS_SQS_LOG: Url da fila de log no SQS da AWS.
- AWS_SQS_GROUP_ID_LOG: Group Id da fila de log no SQS da AWS.
- USER_API_GATEWAY_URL: link do api gateway de pacientes.

## Execução individual do projeto com docker

Para executar com docker, basta executar o seguinte comando na pasta raiz do projeto para gerar a imagem:

``` docker build -t delete_user_info_saga -f .\DeleteUserInfoSaga\Dockerfile . ```

Para subir o container, basta executar o seguinte comando:

``` 
docker run -e AWS_ACCESS_KEY_DYNAMO=""
-e AWS_SECRET_KEY_DYNAMO=""
-e AWS_SQS_LOG=""
-e AWS_SQS_GROUP_ID_LOG=""
-e USER_API_GATEWAY_URL=""
-p 8081:8081 -p 8080:8080 delete_user_info_saga
```

Observação: as variáveis de ambiente não estão com valores para não expor meu ambiente AWS. Para utilizar o serviço corretamente, é necessário definir um valor para as variáveis.

Além disso, como o projeto foi desenvolvido em .NET, também é possível executá-lo pelo Visual Studio ou com o CLI do .NET.
