import assinatura as assinatura_ent
import playlist as playlist_ent

class Usuario:
    
    def __init__(self, nome, sobrenome, cartao_credito):
        self.nome = nome
        self.sobrenome = sobrenome
        self.cartao_credito = cartao_credito
        self.assinaturas = []
        self.playlists = []
        self.bandas_favoritas = []
    
    
    def criar_usuario(cls, nome, sobrenome, cartao_credito):
        usuario = Usuario.criar_usuario(nome, sobrenome, cartao_credito)
        return usuario
    
    def buscar_musicas(self):
        pass
    
    def buscar_bandas(self):
        pass
    
    def assinar_plano(self, plano):
        assinatura = assinatura_ent.Assinatura.criar_assinatura(self, plano)
        self.assinaturas.append(assinatura)
        return assinatura
    
    def criar_playlist(self, nome):
        playlist = playlist_ent.Playlist.criar_playlist(nome, self)
        self.playlists.append(playlist)