CREATE OR REPLACE FUNCTION public.get_mart_tasks(
	wmart_id uuid)
    RETURNS SETOF tasks
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
return query
select t.*
from workspacemart_tasks wt
         right join tasks t on t.id = wt.taskid and t.employeeid is null and t.status = 1
where wt.martid  = wmart_id;
end;
$BODY$;