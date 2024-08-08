DROP PROCEDURE IF EXISTS public.create_user(character, character, character, character);
CREATE OR REPLACE PROCEDURE public.create_user(
	userlogin character,
	userpassword character,
	username character,
	usersurname character)
    LANGUAGE 'plpgsql'

AS $BODY$
BEGIN
INSERT INTO public.users (login, password, name, surname, role) VALUES (userlogin, userpassword, username, usersurname, 2);
END;
$BODY$;