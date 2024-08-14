CREATE OR REPLACE PROCEDURE public.update_user(
	userid uuid,
	userlogin character,
	userpassword character,
	username character,
	usersurname character)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
UPDATE users SET login=userlogin, password=userpassword, name=username, surname=usersurname WHERE id=userid;
END;
$BODY$;