import musicas.bandas_favoritas as bandas_favs_ent
import musicas.playlists as playlists_ent

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