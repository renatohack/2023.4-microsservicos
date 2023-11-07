input_list = ["apple", "banana", "cherry", "date", "kiwi"]
substring = "na"

result = [s for s in input_list if substring in s]
print(result)




input_list = ["apple", "banana", "cherry", "date", "kiwi"]
substring = "na"

result = []
for s in input_list:
    if substring in s:
        result.append(s)
print(result)
