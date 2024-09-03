CREATE OR REPLACE PROCEDURE public.is_manager(IN task_id uuid, IN user_id uuid, OUT is_man bool)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
cnt integer;
BEGIN
SELECT COUNT(*) INTO cnt
FROM tasks
WHERE id = task_id
  AND managerid  = user_id;

IF cnt > 0 THEN
		is_man = true;
ELSE
		is_man = false;
END IF;
END;
$procedure$;