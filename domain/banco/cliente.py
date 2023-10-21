import conta as conta_ent

class Cliente:
    
    
    def __init__(self, nome, sobrenome, cpf, data_nasc, telefone, celular, email, rg, endereco):
        self.nome = nome
        self.sobrenome = sobrenome
        self.cpf = cpf
        self.data_nasc = data_nasc
        self.telefone = telefone
        self.celular = celular
        self.email = email
        self.rg = rg
        self.endereco = endereco
        self.contas = []
        
        
    def cadastrar_cliente(cls, nome, sobrenome, cpf, data_nasc, telefone, celular, email, rg, endereco):
        cliente = Cliente(nome, sobrenome, cpf, data_nasc, telefone, celular, email, rg, endereco)
        return cliente
    
    
    def criar_conta_cliente(self, agencia):
        conta = conta_ent.Conta.criar_conta(agencia, self)
        self.contas.append(conta)