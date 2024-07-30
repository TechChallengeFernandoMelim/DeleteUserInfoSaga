# DeleteUserInfoSaga

Esse reposit�rio tem como objetivo exibir a implementa��o de uma SAGA Orquestrada utilizada para realizar a exclus�o de dados de usu�rio em todos os servi�os dos projetos. 
� necess�rio que seja feito via SAGA orquestrada por causa da ordem de execu��o de servi�os, que deve ser uma ordem bem espec�fica.

## Vari�veis de ambiente
Todas as vari�veis de ambiente do projeto visam fazer integra��o com algum servi�o da AWS ou MercadoPago. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- AWS_SQS_LOG: Url da fila de log no SQS da AWS.
- AWS_SQS_GROUP_ID_LOG: Group Id da fila de log no SQS da AWS.
- USER_API_GATEWAY_URL: link do api gateway de pacientes.

## Execu��o individual do projeto com docker

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

Observa��o: as vari�veis de ambiente n�o est�o com valores para n�o expor meu ambiente AWS. Para utilizar o servi�o corretamente, � necess�rio definir um valor para as vari�veis.

Al�m disso, como o projeto foi desenvolvido em .NET, tamb�m � poss�vel execut�-lo pelo Visual Studio ou com o CLI do .NET.
