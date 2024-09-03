CREATE OR REPLACE FUNCTION public.check_invite(
	martid uuid)
    RETURNS SETOF invites 
    LANGUAGE 'plpgsql'

AS $BODY$

BEGIN
RETURN QUERY SELECT * FROM invites WHERE mart_id=martid;
END;
$BODY$;