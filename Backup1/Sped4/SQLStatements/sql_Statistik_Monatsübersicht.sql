declare @auftraggeber decimal; 
declare @year varchar(4); 
declare @BezugMonat date;
set @year='2014' 
set @auftraggeber=306
--set @BezugMonat = CAST('01.'+MONTH(b.Date)+'.'+@year as Date)

select  
Case  
	when month(date)=1 then 'Januar' 
	when month(date)=2 then 'Februar' 
	when month(date)=3 then 'März' 
	when month(date)=4 then 'April' 
	when month(date)=5 then 'Mai' 
	when month(date)=6 then 'Juni' 
	when month(date)=7 then 'Juli' 
	when month(date)=8 then 'August' 
	when month(date)=9 then 'September' 
	when month(date)=10 then 'Oktober' 
	when month(date)=11 then 'November' 
	when month(date)=12 then 'Dezember'  
	End as Monat
, (select SUM(Netto)/1000 from Artikel where LEingangTableID in (select ID from LEingang where [Check]=1 and DATEDIFF(month,date,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Netto Eingang]
, (select SUM(Netto)/1000 from Artikel where LAusgangTableID in (select ID from LAusgang where Checked=1 and DATEDIFF(month,datum,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Netto Ausgang]
, (select SUM(Netto)/1000 from Artikel where LEingangTableID in (select ID from LEingang where [Check]=1 and DATEDIFF(month,date,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) 
	- (select SUM(Netto)/1000 from Artikel where LAusgangTableID in (select ID from LAusgang where Checked=1 and DATEDIFF(month,datum,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Netto Saldo]

, (select SUM(Brutto)/1000 from Artikel where LEingangTableID in (select ID from LEingang where [Check]=1 and DATEDIFF(month,date,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Brutto Eingang]
, (select SUM(Brutto)/1000 from Artikel where LAusgangTableID in (select ID from LAusgang where Checked=1 and DATEDIFF(month,datum,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Brutto Ausgang]
, (select SUM(Brutto)/1000 from Artikel where LEingangTableID in (select ID from LEingang where [Check]=1 and DATEDIFF(month,date,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) 
	- (select SUM(Brutto)/1000 from Artikel where LAusgangTableID in (select ID from LAusgang where Checked=1 and DATEDIFF(month,datum,'31.01.'+@year)=-MONTH(b.date)+1 and Auftraggeber=@auftraggeber)) as [Brutto Saldo]
--, CAST('01.'+MONTH(b.date)+'.'+@year as nvarchar(20)) as Tage

, 0.0 as [Endbestand] 
--, Date
from Artikel a  
left join leingang b on a.LEingangTableID=b.ID 
left join lausgang c on a.LEingangTableID=c.ID 
group by b.Auftraggeber,month(date)
--, date  
having b.Auftraggeber=@auftraggeber  order by month(date)--, date