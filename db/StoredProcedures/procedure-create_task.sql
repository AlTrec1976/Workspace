CREATE OR REPLACE PROCEDURE public.create_task(
	taskname character,
	taskstatus integer)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
INSERT INTO public.tasks (name, status) VALUES (taskname, taskstatus);
END;
$BODY$;