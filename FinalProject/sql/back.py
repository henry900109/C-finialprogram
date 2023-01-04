# -*- coding: utf-8 -*-
"""
Created on Tue Nov  8 10:38:39 2022

@author: victor
"""

import pandas as pd
import pymysql as db
import matplotlib.pyplot as plt
import matplotlib.dates as mdate
import sys
import time
timestr=time.strftime("%Y%m%d%H%m%S")
def KD(starttime,endtime,symbol,e):
    conn = db.connect(host='localhost',
    user='root',
    password='d29591410',
    database='victor')
    
    cursor=conn.cursor()
    
    highest_price=[]
    lowest_price=[]
    closing_price=[]
    update_time=[]
    klist=[]
    dlist=[]
    
    try:
        sql='select highest_price,lowest_price,closing_price,update_time from stock where stock_symbol=\"'+symbol+'\"and update_time >=\"'+starttime+'\" and update_time <=\"'+endtime+'"'
        cursor.execute(sql)
        row=cursor.fetchall()
        a=list(row)
        
        for i in a:
            if i[3] not in update_time:
                highest_price.append(i[0])
                lowest_price.append(i[1])
                closing_price.append(i[2])
                update_time.append(i[3])
    except:
        print('error')
    
    for i in range(len(update_time)):
        #過去3天
        
        rsv=((closing_price[i]-min(closing_price))/(max(highest_price)-min(closing_price)))*100
        if klist==[]:
            k=(2/3)*50+(1/3)*rsv
        else:
            k=(2/3)*klist[-1]+(1/3)*rsv
            
         
        klist.append(k)
            
            
        if dlist==[]:
            d=(2/3)*50+(1/3)*k
        else:
            d=(2/3)*dlist[-1]+(1/3)*k
        dlist.append(d)
        
   

    return klist,dlist,update_time


def general(symbol,starttime,endtime):
    conn = db.connect(host='localhost',
    user='root',
    password='d29591410',
    database='victor')
    
    cursor=conn.cursor()
    closing_price=[]
    update_time=[]
    
    try:
        sql='select closing_price,update_time from stock where stock_symbol=\"'+symbol+'\"and update_time >=\"'+starttime+'\" and update_time <=\"'+endtime+'"'
        cursor.execute(sql)
        row=cursor.fetchall()
        a=list(row)
        
        for i in a:
            if i[1] not in update_time:
                closing_price.append(i[0])
                update_time.append(i[1])
                
    except:
        print('error')     
                
                
    return closing_price,update_time
                
                

o_symbol  = sys.argv[1] #股票資訊
o_symbol=o_symbol.split(',')
symbol=o_symbol[0]
starttime= sys.argv[2] #開始日期
endtime = sys.argv[3] #結束日期
mod=sys.argv[4]


e=pd.bdate_range(starttime,endtime,freq='b')



if mod == 'KD':
    klist,dlist,update_time=KD(starttime,endtime,symbol,e)           

    plt.plot(klist,'--',color='RED',label="K")
    plt.plot(dlist,color='Green',label='D')
    plt.title(symbol+'K/D')
    ax=plt.gca()
    ax.xaxis.set_major_formatter(mdate.DateFormatter('%Y-%m-%d'))#設定標籤type
    plt.xticks(range(0,len(update_time)))#間距
    plt.xticks(rotation = 25)
    ax.set_xticklabels(labels=update_time)#標籤
    plt.grid(True)
    plt.legend(loc = 'lower left')
    plt.savefig('C:\FinalProject\sql\\'+timestr+'.png')
    print(timestr)
elif mod == 'General':
    closing_price,update_time=general(symbol,starttime,endtime)
    plt.plot(closing_price,color='blue',marker = '.',ms=20)
    plt.title(symbol+'General')
    ax=plt.gca()
    ax.xaxis.set_major_formatter(mdate.DateFormatter('%Y-%m-%d'))#設定標籤type
    plt.xticks(range(0,len(closing_price)))#間距
    plt.xticks(rotation = 25)
    ax.set_xticklabels(labels=update_time)#標籤
    plt.grid(True)
    plt.legend(loc = 'lower left')
    plt.savefig('C:\FinalProject\sql\\'+timestr+'.png')
    print(timestr)

else:
    print('error')
    


