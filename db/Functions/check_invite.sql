CREATE OR REPLACE FUNCTION public.check_invite(
	martid uuid[])
    RETURNS SETOF invites 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
RETURN QUERY SELECT * FROM invites WHERE mart_id=martid;
END;
$BODY$;