var gulp = require('gulp');
var concat = require('gulp-concat');
var sass = require('gulp-sass');
var cleanCSS = require('gulp-clean-css');
var rename = require('gulp-rename');

gulp.task('default', ['sass']);
gulp.task('sass', function (done) {
    gulp.src('./Content/scss/**/*.scss')
        .pipe(sass())
        .on('error', sass.logError)
        .pipe(rename('styles.css'))
        .pipe(gulp.dest('./Content/'))
        .pipe(cleanCSS({
            keepSpecialComments: 0
        }))
        .pipe(rename('styles.min.css'))
        .pipe(gulp.dest('./Content/'))
        .on('end', done);
});

gulp.task('sass:watch',  function  ()  {
    gulp.watch('./Content/scss/**/*.scss',  ['sass']);
});