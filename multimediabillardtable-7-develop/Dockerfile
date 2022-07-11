FROM python:3

EXPOSE 8000

COPY ./ /multimediabillard

WORKDIR /multimediabillard

RUN pip install mkdocs

CMD ["mkdocs", "serve", "--dev-addr=0.0.0.0:8000"]