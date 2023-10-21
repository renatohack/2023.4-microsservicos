class Comerciante:
    
    def __init__(self, cnpj, banco, agencia, conta):
        self.cnpj = cnpj
        self.banco = banco
        self.agencia = agencia
        self.conta = conta
        self.transacoes = []