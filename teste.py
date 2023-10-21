import datetime

today = datetime.date.today()
date = today + datetime.timedelta(days = 5 * 365)

print(date)