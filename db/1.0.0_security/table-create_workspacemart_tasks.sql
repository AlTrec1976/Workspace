CREATE TABLE public.workspacemart_tasks (
	martid uuid NOT NULL,
	taskid uuid NOT NULL
);

ALTER TABLE public.workspacemart_tasks ADD CONSTRAINT mart_mart_fk FOREIGN KEY (martid) REFERENCES public.workspacemart(id);
ALTER TABLE public.workspacemart_tasks ADD CONSTRAINT mart_task_fk FOREIGN KEY (taskid) REFERENCES public.tasks(id);