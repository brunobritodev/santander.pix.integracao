Demo para gerar Token de integração de PIX com o banco Santander.
------------------

O .NET por baixo do capô usa as API's do Windows para chamadas http's. E no caso do santander devido ao tamanho da chave de criptografia o seguinte erro ocorre: `The message received was unexpected or badly formatted`.

Essa mensagem não revela nada. Além disso direciona o dev a ir por caminhos errados.

Com essa mensagem de erro nenhum material realmente útil é encontrado por ai.

Esse código identifica esse cenário e ajuda o dev a resolver.
