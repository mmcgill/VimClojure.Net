# VimClojure.Net

The VimClojure server, ported to .Net/ClojureCLR.

First, some disclaimers:

**This is a work-in-progress.**

Lots of functionality (including REPL!) doesn't work yet, and what
does work has barely been tested. This isn't even Alpha quality yet.
When it is, I'll build some binaries.

**This project was deprecated before I started it.**

VimClojure is being split into a
[static part](https://github.com/guns/vim-clojure-static)
and a dynamic part,
and future development on the dynamic part
[may be leisurely](https://groups.google.com/forum/#!topic/vimclojure/B-UU8qctd5A/discussion).
The word on the street is that the cool kids are using
[fireplace.vim](https://github.com/tpope/vim-fireplace) now.

That said, VimClojure still works. This project is for the devs
(all four of them) that

* haven't figured out Emacs yet, and
* prefer VimClojure's dynamic features to fireplace.vim, and
* have to get our Clojure fix on the CLR, for one reason or another.

## Prerequisits

* [ClojureCLR](): Make sure to set the `CLOJURE_LOAD_PATH` environment variable
  to the path to your ClojureCLR installation.

* [VimClojure](https://bitbucket.org/kotarak/vimclojure): Follow the the setup
  instructions for the vim plugin and the Nailgun client (ng).

* Visual Studio 2012 Express

## Installation and Usage

Again, this is for development only right now.

1. Clone this repository.

2. Open VimClojure.Net.sln in VS2012 Express, and build it.

3. In the VimClojure.Server project properties, set the
   working directory to be the root directory containing the Clojure source
   for whatever project you're hacking on.

4. Ctrl-F5

5. Fire up Vim and enjoy your interactivity.


