import assinatura as assinatura_ent
import playlist as playlist_ent

class Usuario:
    
    def __init__(self, nome, sobrenome, cartao_credito):
        self.nome = nome
        self.sobrenome = sobrenome
        self.cartao_credito = cartao_credito
        self.assinaturas = []
        self.musicas_favoritas = playlist_ent.Playlist.criar_playlist('Favoritas', self)
        self.playlists = []
        self.bandas_favoritas = []
    
    def criar_usuario(cls, nome, sobrenome, cartao_credito):
        usuario = Usuario.criar_usuario(nome, sobrenome, cartao_credito)
        return usuario
    
    
    def assinar_plano(self, plano):
        assinatura = assinatura_ent.Assinatura.criar_assinatura(self, plano)
        self.assinaturas.append(assinatura)
        return assinatura
    
    
    def criar_playlist(self, nome):
        playlist = playlist_ent.Playlist.criar_playlist(nome, self)
        self.playlists.append(playlist)
    
    
    def favoritar_banda(self, banda):
        self.bandas_favoritas.append(banda)
        banda.usuarios.append(self)