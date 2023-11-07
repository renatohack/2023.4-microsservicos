class BandasFavoritas:
    
    def __init__(self, usuario):
        self.usuario = usuario
        self.lista_bandas = []
    
    
    def favoritar_banda(self, banda):
        self.lista_bandas.append(banda)
        banda.registrar_banda_fav(self)
        
        
    def buscar_bandas(self, nome):
        return [banda.nome for banda in self.lista_bandas if nome in banda.nome]