import plano.assinatura as assinatura_ent

class Assinaturas:
    
    def __init__(self, usuario):
        self.usuario = usuario
        self.assinaturas = []
    
    
    def assinar_plano(self, plano):
        
        cartao_usuario = self.usuario.cartao_credito
        if (not Assinaturas.validar_cartao(cartao_usuario)):
            return None, "Cartão de crédito inválido."
        
        
        if (len(self.assinaturas) > 0):
            ultima_assinatura = self.assinaturas[-1]
            ultima_assinatura.cancelar_assinatura()
        
        assinatura = assinatura_ent.Assinatura.criar_assinatura(self, plano)
        self.assinaturas.append(assinatura)
        return assinatura, "Plano assinado."
    
    
    def validar_cartao(cls, cartao):
        pass