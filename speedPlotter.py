import matplotlib.pyplot as plt
import numpy as np

PATH = "C:\\Users\\Armand\\Desktop\\"
FILENAME = "CBallSpeed.txt"

NAME = PATH+FILENAME
NAME_USED = NAME[NAME.rfind("\\")+1:len(NAME)-4:]
deltaS = -1

speeds = []
times = []

if __name__ == "__main__":

    with open (NAME,"r") as file:
        data = file.read().split("\n")
        data.pop(len(data)-1)
        
    for item in data:
        (s,t) = item.split(" | ")
        deltaS = float(t) if deltaS < 0 else deltaS
        speeds.append(s)
        times.append(float(t)-deltaS)

    speeds = np.array(speeds,dtype=float)
    times = np.array(times, dtype=float)

    fig = plt.figure(figsize = (8,8))
    ax = plt.axes()
    ax.grid()

    ax.plot(times, speeds, c = 'r')
    ax.set_title(NAME_USED)

    ax.set_xlabel("time", labelpad=20)
    ax.set_ylabel("speed", labelpad=20)
    

    fig.savefig(fname=NAME[:len(NAME)-4:])

    

    # plt.bar(range(len(colours)), list(colours.values()), align='center')
    # plt.xticks(range(len(colours)), list(colours.keys()))
    # plt.title(_fname :=smoothen(colours,data))
    # #plt.show()
    # plt.savefig(fname="".join(_fname[:len(_fname)-1:].split()).replace(":"," ")+".png")
    









































