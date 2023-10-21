class Conta:
    
    id_controle = 1
    
    def __init__(self, agencia, cliente):
        self.agencia = agencia
        self.cliente = cliente
        self.cartoes = []
        
        self.numero = Conta.id_controle
        Conta.id_controle = Conta.id_controle + 1
        
    
    def criar_conta(cls, agencia, cliente):
        conta = Conta(agencia, cliente)
        return conta