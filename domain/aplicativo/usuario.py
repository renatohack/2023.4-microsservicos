import assinatura as assinatura_ent
import playlist as playlist_ent
import bandas_favoritas as bandas_favs_ent
import playlists as playlists_ent

class Usuario:
    
    def __init__(self, nome, sobrenome, cartao_credito):
        
        # dados usuario
        self.nome = nome
        self.sobrenome = sobrenome
        self.cartao_credito = cartao_credito
        
        # assinatura
        self.assinaturas = []
        
        # playlists e bandas
        self.playlists = playlists_ent.Playlists(self)
        self.bandas_favoritas = bandas_favs_ent.BandasFavoritas(self)
    
    
    def criar_usuario(cls, nome, sobrenome, cartao_credito):
        usuario = Usuario.criar_usuario(nome, sobrenome, cartao_credito)
        return usuario
    
    
    def assinar_plano(self, plano):
        assinatura = assinatura_ent.Assinatura.criar_assinatura(self, plano)
        self.assinaturas.append(assinatura)
        return assinatura