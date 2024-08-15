CREATE OR REPLACE FUNCTION public.check_invite(
	martid uuid[])
    RETURNS SETOF sendbox 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

BEGIN
RETURN QUERY SELECT * FROM sendbox WHERE mart_id=martid;
END;
$BODY$;