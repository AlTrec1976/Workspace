CREATE TABLE public.workspacemart_users (
	martid uuid NOT NULL,
	userid uuid NOT NULL
);

ALTER TABLE public.workspacemart_users ADD CONSTRAINT mart_mart_fk FOREIGN KEY (martid) REFERENCES public.workspacemart(id);
ALTER TABLE public.workspacemart_users ADD CONSTRAINT mart_user_fk FOREIGN KEY (userid) REFERENCES public.users(id);