class Banda:
    
    def __init__(self, nome, musicas):
        self.nome = nome
        self.musicas = musicas
        self.bandas_favs_registradas = []
        
        
    def buscar_musicas(self, nome):
        return [musica.nome for musica in self.musicas if nome in musica.nome]
    
    
    def registrar_banda_fav(self, banda_fav):
        self.bandas_favs_registradas.append(banda_fav)