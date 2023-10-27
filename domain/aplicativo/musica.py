class Musica:
    
    def __init__(self, nome, banda):
        self.nome = nome
        self.banda = banda
        self.playlists = []
        
    
    def registrar_playlist(self, playlist):
        self.playlists.append(playlist)