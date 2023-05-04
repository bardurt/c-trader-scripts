# c-trader-scripts
This repository contains a list of scripts, indicators, for cTrader. These Scripts can be copied into cTrader Script editor and added to any chart!

</br>

If you want to know how to add cutom scripts to cTrader please click [here](https://github.com/bardurt/c-trader-scripts/blob/main/help/tutorial.md)

</br>


## 1 - Fair Value Gap
The Fair Value Gap indicator is based on concepts by <b>Michael J. Huddleston</b> - https://www.youtube.com/c/InnerCircleTrader. It is a 3 bar pattern, bar 1, 2 and 3, where bar 1 and 3 do not intersect eachother. This forms a Fair Value Gap in bar 2 in the space between bar 1 and 3.

<img src="https://github.com/bardurt/c-trader-scripts/blob/main/fvg/fvg.png" width="400" height="400">


#### Location
fvg/FairValueGap.cs

#### Preview
<img src="https://github.com/bardurt/c-trader-scripts/blob/main/fvg/fvg_sp500.png" width="512" height="512">
</br>
</br>


## Silver Bullet
Silver bullet are are set ups that occur between [ 10:00 - 11:00 ] and [ 14:00 - 15:00 ] NY time. During this time frames price will reaveal a move. 
