# mkdocs

mkdocs is a tool to run your documentation (.md files) as website in your browser. 

## Requirements

You need Python, pip and mkdocs installed or within a docker container. 

We used docker for that. 
The Dockerfile installs Python, pip and mkdocs within the container and runs mkdocs serve to start it.

* run `docker-compose up` or `docker-compose up -d` in the multimediabillard root folder (`multimediabillardtable-7` in our case)

* open up `http://localhost:8000/` in your browser (or whatever you have configured as site_url)

## Usage

* modify title, url and theme in mkdocs.yml

* all subfolders and all of the .md files inside of the docs-directory are the content of the generated website

* if not declared in another way in mkdocs.yml, the page navigation is the same as your folder structure

## General information for using mkdocs

* documentation should be written as regular Markdown files (.md)

* the documentation directory will be named `docs` by default

* you need a mkdocs.yml at the same level as the docs folder

### Project layout

    mkdocs.yml    # The configuration file.
    docs/
        index.md  # The documentation homepage.
        ...       # Other directories, markdown pages, images and other files.

## Styling

You can make different configurations and changes for your styling. 
For detailed help go and visit [mkdocs-material](https://squidfunk.github.io/mkdocs-material/setup/changing-the-colors/).

## Hint for docker usage

Currently the docker container and image has to be completely removed and restarted everytime you made changes in your documentation because the files are copied into the container on creating the image. 
