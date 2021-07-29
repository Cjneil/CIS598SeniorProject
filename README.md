# K-State CS Codio to Hugo Converter

This is a project for converting Codio Textbooks to a [Hugo](https://gohugo.io/)-based web framework for K-State CS lecture content. 
It uses a lightly adapted version of the [Hugo-theme-learn](https://learn.netlify.com/en/) theme.

Major added features by conversion to the Hugo framework are the addition of [Reveal.js](https://github.com/hakimel/reveal.js/) slideshow framework, and the creation of an embeddable version of content pages for use with IFrames in [Canvas](https://www.instructure.com/) and other learning management systems.

**[Live Site](https://textbooks.cs.ksu.edu/cis527)**

#### Relevant Documentation

* [Hugo Documentation](https://gohugo.io/documentation/)
* [Hugo-theme-learn Documentation](https://learn.netlify.com/en/)
* [Reveal.js Documentation](https://github.com/hakimel/reveal.js/)

## Creating a Hugo Textbook from existing Codio book

Install Hugo > 0.47 on your system. I recommend using Linux or Windows Subsystem for Linux.

Install or confirm that you have Microsoft Visual Studio or another method to run a Visual Studio solution.

Install git or confirm existing git installation (only needed to automate download of ksucs-hugo-theme so technically optional)

Clone this repository and open the solution within Visual Studio.

Confirm the location of your existing Codio Textbook. This should consist of a primary directory with a .guides directory containing 
at minimum a content directory with the content of the textbook, img containing images used in the content, metadata.json, and books.json.

Create or identify an empty directory for use as a target directory for the application.

Run the visual studio solution which pulls up a UI allowing you to select the Codio Source identified prior and the Hugo target which will be the empty directory.

Once both are selected, if the file paths show up with no errors then you will be able to click Create Hugo textbook. This will do a good 90% of the work for you.

Please note: Assessments, quizzes, additional non-page files will not be converted due to a lack of Hugo equivalent. Creating a replacement will need to be done manually. 
Additionally, all "Teacher only" sections/pages will not be converted

Once the box on the UI shows that the textbook has been converted, open the directory you asked it to create the textbook in and run getTheme.sh
This will clone the ksucs-hugo-theme from https://github.com/russfeld/ksucs-hugo-theme into themes/ksucs-hugo-theme

Alternatively, you may simply download ksucs-hugo theme from the above link and drag it into the themes directory of the textbook manually.

At this point, all that is left for you to do is modify the config.toml file for the textbook, which I have included an example of the structure for in this repo.
Additionally I would highly suggest filling out the _index.md files for each chapter/section as they are created with default text and the placeholder in /layouts/partials/logo.html

Once the config is filled out, you can use hugo server or hugo commands to test your Hugo site.

To view the content locally, use the `hugo server` command and visit http://localhost:1313 to view a local version of your site

To deploy the content, use the `hugo` command on the destination server to generate a `public` folder. Then, point your web server of choice to that folder.

## Questions?

Contact [Connor Neil] - cjneil@ksu.edu
