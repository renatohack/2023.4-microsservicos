class Playlist:
    
    def __init__(self, nome, playlists):
        self.nome = nome
        self.playlists = playlists
        self.musicas = []
    
    def criar_playlist(cls, nome, usuario):
        playlist = Playlist(nome, usuario)
        return playlist
    
    
    def adicionar_musica(self, musica):
        self.musicas.append(musica)
        musica.registrar_playlist(self)
    
    
    def buscar_musicas(self, nome):
        return [musica.nome for musica in self.musicas if nome in musica.nome]