/// <binding BeforeBuild='auto' />
module.exports = function (grunt) {
    grunt.initConfig({

        clean: ["wwwroot/miniJS/site.min.js", "wwwroot/miniCSS/site.min.css", "temp/*"],

        concat: {
            all: {
                src: ['wwwroot/js/**/*.js'],
                dest: 'temp/combined.js'
            }
        },
        jshint: {
            files: ['temp/combined.js'],
            options: {
                '-W069': false
            }
        },

        uglify:
        {
            js:
            {
                src: ['temp/combined.js'],
                dest: 'wwwroot/miniJS/site.min.js'
            }
        },

        cssmin:
        {
            css:
            {
                src: ['wwwroot/css/**/*.css'],
                dest: 'wwwroot/miniCSS/site.min.css'
            }
        },

        watch: {
            files: ["wwwroot/css/**/*.css", "wwwroot/js/**/*.js"],
            tasks: ["auto"]
        }
    });

    grunt.registerTask("auto", ['clean', 'concat', 'jshint', 'uglify', 'cssmin']);

    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-watch');

};