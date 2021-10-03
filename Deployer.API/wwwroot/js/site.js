(function init() {
    let versionTemplate = _.template($("#versionRowTemplate").html())
    $.get("/packages/versions").done(x => {
        for (let version of x) {
            console.log(version)
            console.log(versionTemplate(version))
            $(".jsVersions").append(versionTemplate(version))
        }
    })
})()