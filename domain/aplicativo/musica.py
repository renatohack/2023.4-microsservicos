class Musica:
    
    def __init__(self, nome, banda):
        self.nome = nome
        self.banda = banda
        self.playlists_registradas = []
        
    
    def registrar_playlist(self, playlist):
        self.playlists_registradas.append(playlist)