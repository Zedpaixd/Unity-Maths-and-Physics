import matplotlib.pyplot as plt
import sys

PATH = "C:\\Users\\Armand\\Desktop\\"
FILENAME = "CollisionStats.txt"

NAME = PATH+FILENAME
NAME_USED = NAME[NAME.rfind("\\")+1:len(NAME)-4:]

colours = { "green" : 0,
            "yellow" : 0,
            "red" : 0,
            "black" : 0}


def smoothen(dic,line1):
    line1 = line1.split(" ")
    line1[1] = str((10**(len(str(sum([int(a) for a in dic.values()])))-2)) * round(sum([int(a) for a in dic.values()])/(10**(len(str(sum([int(a) for a in dic.values()])))-2))))
    return "".join(line1).replace("-"," ") 

if __name__ == "__main__":

    with open (NAME,"r") as file:
        data = file.readline()
        data2 = file.read().replace(" (Instance)","").split("\n")
        
    for color in data2[:len(data2)-1:]:
        colours[color.lower().strip()] += 1

    

    plt.bar(range(len(colours)), list(colours.values()), align='center')
    plt.xticks(range(len(colours)), list(colours.keys()))
    plt.title(_fname :=smoothen(colours,data))
    #plt.show()
    plt.savefig(fname="".join(_fname[:len(_fname)-1:].split()).replace(":"," ")+".png")
    









































