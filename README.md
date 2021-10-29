# API-Active-Directory

> Versão mais recente: v1

API para autenticação de usuários no AD. É capaz de enviar um login e senha para o Active Directory e retornar se confere com o cadastro existente.

Todas as aplicações que requerem autenticação através de Active Directory poderão consumir esta API seguindo as seguintes premissas:

1º - A aplicação solicitante deverá ser previamente informada à equipe mantenedora desta API pois é necessário que o host cliente seja liberado, pois somente hosts habilitados poderão utilizar os endpoints aqui contidos.

2º - O endpoint de autenticação é /api/v1/Security/Login
	Aguarda UserName e Password via corpo da requisição em formato json conforme exemplo:
	
{
  "username": "string",
  "password": "string"
}

3º As respostas possíveis são:

 Code: 200 em caso de sucesso.
 Code: 401 em caso de erro.