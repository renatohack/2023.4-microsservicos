class Assinatura:
    
    def __init__(self, usuario, plano):
        self.usuario = usuario
        self.plano = plano
        self.assinatura_ativa = True
    
    def criar_assinatura(cls, usuario, plano):
        assinatura = Assinatura(usuario, plano)
        plano.incluir_assinatura(assinatura)
        return assinatura