class Plano:
    
    def __init__(self, nome, valor):
        self.nome = nome
        self.valor = valor
        self.assinaturas = []
    
    def incluir_assinatura(self, assinatura):
        self.assinaturas.append(assinatura)