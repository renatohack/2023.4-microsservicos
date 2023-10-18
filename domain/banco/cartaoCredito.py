class CartaoCredito:
    
    id_controle = 1
    
    def __init__(self, conta):
        # valores args
        self.conta = conta
        self.transacoes = []
        
        # valores default
        self.cartaoAtivo = False
        self.limiteDisponivel = 0.00
        self.nome = conta.cliente.nome.upper() + " " + conta.cliente.sobrenome.upper()
        
        # valores que se utilizam de metodos
        self.validade = "01/01/1900" # PEGAR DATA ATUAL E ADICIONAR X ANOS
        self.cvv = "000" # ADICIONAR NUMERO 3 DIGITOS COM RANDOM
        
        # valores que se utilizam de static
        self.numero = CartaoCredito.id_controle
        CartaoCredito.id_controle = CartaoCredito.id_controle + 1