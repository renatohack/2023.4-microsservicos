class Playlist:
    
    def __init__(self, nome, usuario):
        self.nome = nome
        self.usuario = usuario
        self.musicas = []
    
    def criar_playlist(cls, nome, usuario):
        playlist = Playlist(nome, usuario)
        return playlist
    
    def adicionar_musica(self, musica):
        self.musicas.append(musica)