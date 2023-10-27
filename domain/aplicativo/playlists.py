import playlist as playlist_ent

class Playlists:
    
    def __init__(self, usuario):
        self.usuario = usuario
        favoritas = playlist_ent.Playlist.criar_playlist('Favoritas', self)
        self.playlists = [favoritas]
        
        
    def criar_playlist(self, nome):
        playlist = playlist_ent.Playlist.criar_playlist(nome, self)
        self.playlists.append(playlist)
    
    
    def favoritar_musica(self, musica):
        playlist = self.playlists[0]
        playlist.adicionar_musica(musica)
    
    
    def buscar_playlists(self, nome):
        return [playlist.nome for playlist in self.playlists if nome in playlist.nome]