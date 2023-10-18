import conta as conta_ent

class Cliente:
    
    
    def __init__(self, nome, sobrenome, cpf, dataNascimento, telefone, celular, email, rg, endereco):
        self.nome = nome
        self.sobrenome = sobrenome
        self.cpf = cpf
        self.dataNascimento = dataNascimento
        self.telefone = telefone
        self.celular = celular
        self.email = email
        self.rg = rg
        self.endereco = endereco
        self.contas = []
        
        
    def cadastrarCliente(nome, sobrenome, cpf, dataNascimento, telefone, celular, email, rg, endereco):
        cliente = Cliente(nome, sobrenome, cpf, dataNascimento, telefone, celular, email, rg, endereco)
        return cliente
    
    
    def criarContaUsuario(self, agencia):
        conta = conta_ent.Conta.criarConta(agencia, self)
        self.contas.append(conta)