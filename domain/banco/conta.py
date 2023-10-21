import cartao_credito as cartao_ent

class Conta:
    
    id_controle = 1
    
    def __init__(self, agencia, cliente):
        self.agencia = agencia
        self.cliente = cliente
        self.cartoes = []
        
        self.numero = Conta.id_controle
        Conta.id_controle = Conta.id_controle + 1
        
    
    def gerar_conta(cls, agencia, cliente):
        conta = Conta(agencia, cliente)
        return conta
    
    def adicionar_cartao_credito(self):
        cartao = cartao_ent.CartaoCredito.gerar_cartao_credito(self)
        self.cartoes.append(cartao)