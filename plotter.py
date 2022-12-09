from unicodedata import name
from mpl_toolkits import mplot3d
import numpy as np
import matplotlib.pyplot as plt
import sys


PATH = "C:\\Users\\Armand\\Desktop\\"
FILENAME1 = "BallRecreated.txt"
FILENAME = "Ball.txt"
FILENAMESPHERE1 = "BlueSphere.txt"
FILENAMESPHERE2 = "GreenSphere.txt"
FILENAMESPHERE3 = "RedSphere.txt"

NAME = sys.argv[1] # PATH+FILENAMESPHERE3
NAME_USED = NAME[NAME.rfind("\\")+1:len(NAME)-4:]

coords, speeds, times = [],[],[]
xdata,ydata,zdata = [],[],[]


def plotOnlyTwo(data1,data2,axis1,axis2, name):

    lack = "xyz".replace(axis1,"").replace(axis2,"")

    fig = plt.figure(figsize = (8,8))
    ax = plt.axes()
    ax.grid()

    ax.plot(data1, data2, c = 'r')
    ax.set_title(NAME_USED)

    ax.set_xlabel(axis1, labelpad=20)
    ax.set_ylabel(axis2, labelpad=20)

    fig.savefig(fname=NAME[:len(NAME)-4:]+name+(" figure no {}.png".format(lack) if lack is not "xyz" else ""))


def plotCoordinates(xdata,ydata,zdata, name):
    fig = plt.figure(figsize = (8,8))
    ax = plt.axes(projection='3d')
    ax.grid()

    ax.scatter(xdata, ydata, zdata, c = 'r', s = 5)
    ax.set_title(NAME_USED)

    ax.set_xlabel('x', labelpad=20)
    ax.set_ylabel('y', labelpad=20)
    ax.set_zlabel('z', labelpad=20)

    fig.savefig(fname=NAME[:len(NAME)-4:]+ name + " figure.png")

    plotOnlyTwo(xdata,ydata,"x","y",name +  "figure no z.png")
    plotOnlyTwo(xdata,zdata,"x","z",name +  "figure no y.png")
    plotOnlyTwo(ydata,zdata,"y","z",name +  "figure no x.png")


if __name__ == "__main__":

    with open (NAME,"r") as file:
        data = file.read().replace("(","").replace(")","").split("\n")
        size = len(data)-1
        data.pop(len(data)-1)

        
        try:
            for CnS in data:
                tempC,tempS,_times = CnS.split(" | ")
                coords.append(tempC)
                speeds.append(tempS)
                times.append(_times.strip())

            for lineOfData in coords:
                x,y,z = lineOfData.split(",")
                xdata.append(x)
                ydata.append(y)
                zdata.append(z)
                
        except:
            for lineOfData in data:
                x,y,z = lineOfData.split(",")
                xdata.append(x)
                ydata.append(y)
                zdata.append(z)


        zdata = np.array(zdata,dtype=float)
        xdata = np.array(xdata,dtype=float)
        ydata = np.array(ydata,dtype=float)
        speeds = np.array(speeds,dtype=float)
        times = np.array(times, dtype=float)

        plotCoordinates(xdata,ydata,zdata, "coords")
        plotOnlyTwo(times,speeds,"time in seconds","Speed"," Speed figure.png")