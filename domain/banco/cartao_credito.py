import datetime
from random import randint

class CartaoCredito:
    
    id_controle = 1
    
    def __init__(self, conta):
        # valores args
        self.conta = conta
        self.transacoes = []
        
        # valores default
        self.cartao_ativo = False
        self.limite_disp = 0.00
        self.nome = conta.cliente.nome.upper() + " " + conta.cliente.sobrenome.upper()
        
        # valores que se utilizam de metodos
        self.validade = CartaoCredito.__gerar_data_validade()
        self.cvv = CartaoCredito.__gerar_cvv()
        
        # valores que se utilizam de static
        self.numero = CartaoCredito.id_controle
        CartaoCredito.id_controle = CartaoCredito.id_controle + 1
        
        
    def __gerar_data_validade(cls):
        return datetime.date.today() + datetime.timedelta(days = 5 * 365)
    
    def __gerar_cvv(cls):
        return str(randint(0, 999)).zfill(3)
    
    def gerar_cartao_credito(cls, conta):
        cartao = CartaoCredito(conta)
        return cartao