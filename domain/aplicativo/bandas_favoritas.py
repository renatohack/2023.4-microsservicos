class BandasFavoritas:
    
    def __init__(self, usuario):
        self.usuario = usuario
        self.bandas_favoritas = []
        
        
    def buscar_bandas(self):
        pass
    
    
    def favoritar_banda(self, banda):
        self.bandas_favoritas.append(banda)
        banda.bandas_favoritas.append(self)