<template>
  <div>
    <section class="bg-primary" id="about">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                    <h2 class="display-2">We've got what you need!</h2>
                    <p class="text-faded">Start Bootstrap has everything you need to get your new website up and running in no time! All of the templates and themes on Start Bootstrap are open source, free to download, and easy to use. No strings attached!</p>
                    <a href="#services" class="page-scroll btn btn-default btn-xl sr-button">Get Started!</a>
                </div>
            </div>
        </div>
    </section>

    <section id="services">
      <div class="container">
        <div class="row">
            <div class="col-lg-12 text-center">
                <h2 class="display-1">At Your Service</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-6 text-center">
                <div class="service-box">
                    <i class="fa fa-4x fa-diamond text-primary sr-icons"></i>
                    <h3>Sturdy Templates</h3>
                    <p class="text-muted">Our templates are updated regularly so they don't break.</p>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 text-center">
                <div class="service-box">
                    <i class="fa fa-4x fa-paper-plane text-primary sr-icons"></i>
                    <h3>Ready to Ship</h3>
                    <p class="text-muted">You can use this theme as is, or you can make changes!</p>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 text-center">
                <div class="service-box">
                    <i class="fa fa-4x fa-newspaper-o text-primary sr-icons"></i>
                    <h3>Up to Date</h3>
                    <p class="text-muted">We update dependencies to keep things fresh.</p>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 text-center">
                <div class="service-box">
                    <i class="fa fa-4x fa-heart text-primary sr-icons"></i>
                    <h3>Made with Love</h3>
                    <p class="text-muted">You have to make your websites with love these days!</p>
                </div>
            </div>
        </div>
      </div>
    </section>

    <section class="features mt-5">
      <div class="container">
        <div class="row">
          <div class="col-lg-5 ml-auto">
            Hello
          </div>
          <div class="col-lg-5 mr-auto">
            Is it me you're looking for?
          </div>
        </div>
        <div class="row">
          <div class="col-lg-5 ml-auto">
            Hello
          </div>
          <div class="col-lg-5 mr-auto">
            Is it me you're looking for?
          </div>
        </div>
      </div>
    </section>
    <section class="features mt-5">
      <div class="container">
        <div class="row">
          <div class="col-lg-5 ml-auto">
            Hello
          </div>
          <div class="col-lg-5 mr-auto">
            Is it me you're looking for?
          </div>
        </div>
        <div class="row">
          <div class="col-lg-5 ml-auto">
            Hello
          </div>
          <div class="col-lg-5 mr-auto">
            Is it me you're looking for?
          </div>
        </div>
      </div>
    </section>

    <section id="pricing" class="mt-5">
      <h2 class="display-2 grey--text text--darken-1 text-center">Price plans</h2>
      <p class="text-center">As we're new, we haven't decided on pricing yet.  Sign up today and get a minimum of 12 months membership for free.</p>
      <pricing></pricing>
    </section>

    <aside class="bg-dark">
      <div class="text-center">
        <div class="call-to-action">
          <h2>Free Download at Start Bootstrap!</h2>
          <a href="http://startbootstrap.com/template-overviews/creative/" class="btn btn-default btn-xl sr-button">Download Now!</a>
        </div>
      </div>
    </aside>

    <section id="contact">
      <div class="row">
          <div class="col-lg-8 col-lg-offset-2 text-center">
              <h2 class="section-heading">Let's Get In Touch!</h2>
              <hr class="primary">
              <p>Ready to start your next project with us? That's great! Give us a call or send us an email and we will get back to you as soon as possible!</p>
          </div>
          <div class="col-lg-4 col-lg-offset-2 text-center">
              <i class="fa fa-phone fa-3x sr-contact"></i>
              <p>123-456-6789</p>
          </div>
          <div class="col-lg-4 text-center">
              <i class="fa fa-envelope-o fa-3x sr-contact"></i>
              <p><a href="mailto:your-email@your-domain.com">feedback@startbootstrap.com</a></p>
          </div>
      </div>
    </section>
  </div>
</template>

<style lang="scss" scoped>
  .features {
    padding: 50px 0;
    background: #fff;
    &:nth-child(even) {
      background-color: #f8f8f8;
      border-top: 1px solid #e7e7e7;
      border-bottom: 1px solid #e7e7e7;
    }
  }
</style>

