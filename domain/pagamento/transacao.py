import datetime

class Transacao:
    
    def __init__(self, pagador, recebedor, valor, horario):
        self.pagador = pagador
        self.recebedor = recebedor
        self.valor = valor
        self.horario = horario
        
    
    def autorizar_transacao(cls, pagador, recebedor, valor):
        
        horario_atual = datetime.datetime.now()
        
        # Verifica se cartao esta ativo
        if (not pagador.cartao_ativo):
            return False, "Cartão bloqueado."
        
        # Verifica se há limite disponivel
        if (valor > pagador.limite_disp):
            return False, "Limite insuficiente."
        
        # Verifica se há mais de 3 transacoes em 2 minutos
        transacoes = pagador.transacoes
        if (len(transacoes >= 3)):
            
            horario_antepenultima_transacao = transacoes[-3].horario
            
            if (horario_atual - horario_antepenultima_transacao < datetime.timedelta(minutes = 2)):
                return False, "Transação bloqueada. Alta frequência de transações."
            
        # Verifica se é uma transação semelhante
        ultima_transacao = transacoes[-1]
        ultimo_valor = ultima_transacao.valor
        ultimo_cnpj = ultima_transacao.recebedor.cnpj
        ultimo_horario = ultima_transacao.horario
        
        if (horario_atual - ultimo_horario < datetime.timedelta(minutes = 2) and 
            ultimo_valor == valor and 
            ultimo_cnpj == recebedor.cnpj):
            return False, "Transação bloqueada. Transação duplicada."
        
        return True, "Transação aceita.", horario_atual
    
    
    def gerar_transacao(cls, pagador, recebedor, valor, horario):
        transacao = Transacao(pagador, recebedor, valor, horario)
        return transacao
    
    def realizar_transacao(cls, pagador, recebedor, valor):
        
        transacao_autorizada, detalhes, horario = Transacao.autorizar_transacao(pagador, recebedor, valor)
        
        if(transacao_autorizada):
            transacao = Transacao.gerar_transacao(pagador, recebedor, valor, horario)
            pagador.transacoes.append(transacao)
            recebedor.transacoes.append(transacao)
            
        print("{0} - {1}".format(detalhes, horario))